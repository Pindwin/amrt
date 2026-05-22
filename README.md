## Obserwacje projektowe

- Mamy do czynienia z aplikacją sieciową z elementem kompetetywnego PvP i chcemy zabezpieczyć się przed cheatowaniem. Najprostszym rozwiązaniem będzie więc przeniesienie części odpowiedzialności na autorytatywny serwer.
- Możliwość gry PvE mogłaby skłaniać, aby jej całość odbywała się lokalnie. Leaderboardy PvE wprowadzają jednak również do tego trybu element kompetetywnego PvP - oznacza to, że rozgrywka PvE również powinna odbywać się na serwerze.
- Kontekst zadania sugeruje, że zalecaną technologią klienta gry jest Unity; to powiedziawszy, aplikacja serwerowa Unity do niczego nie potrzebuje. Rozsądnym (choć nie jedynym) rozwiązaniem wydaje się wydzielenie części kodu gry jako GameplayRuntime, pozwalającego na procesowanie jej w sposób niezależny od klienta gry. Ma to dodatkową zaletę możliwości współdzielenia go między kodem serwera i klienta - w ten sposób, klient może walidować proponowane przez gracza ruchy zgodnie ze wspólną z serwerem logiką. Próba oszustwa zostanie wyłapana przez serwer autorytatywny, ale uczciwy gracz może otrzymać natychmiastowy feedback o nielegalności ruchu (bez użycia sieci).

### LobbyServer
- Połączenie leaderboardów oraz możliwości ponownego dołączania do gry wskazuje na to, że każda gra istnieje w szerszym kontekście meta-rozgrywki. Wymagania nie wskazują na to wprost, ale konieczne będzie utrzymanie przynajmniej minimalnej informacji na temat tożsamości gracza. Nie musi to oznaczać logowania/autentykacji; w teorii, wystarczyłby prompt o podanie imienia podczas uruchomienia klienta (tak proste rozwiązanie powodowałoby jednak problemy z duplikatami w leaderboardach). Na potrzeby projektu, zakładam, że uruchomienie klienta jest tożsame z powołaniem do życia obiektu `IAuth`; szczegóły implementacji będą tu zależne od wymagań biznesowych.
- Klasycznym rozwiązaniem ww. problemu jest podzielenie części serwerowej na meta-serwer (LobbyServer) oraz pomniejsze procesy rozgrywki (GameServer). W zależności od wymagań biznesowych, mogłoby być możliwe ograniczenie się wyłącznie do LobbyServer i zawarcie w nim wszystkich rozgrywek. Ma to tę zaletę, że zmniejsza tarcie związane z nawiązywaniem dodatkowych połączeń sieciowych - należy jednal wziąć pod uwagę planowaną ilość nawiązywanych połączeń oraz możliwości sprzętowe serwera. Rozwiązanie z dodatkowymi procesami ma tę zaletę, że zdecydowanie lepiej się skaluje dla dużej ilości graczy.
- LobbyServer przechowuje więc listę rozgrywek PvP oraz PvE. Opuszczenie rozgrywki PvP nie powoduje jej przegranej - logika serwera powinna być niezależna od statusu połączenia klienta. W przypadku PvE, wymagania tego nie definiują, ale warto uczulić biznes na koszty związane z utrzymywaniem porzuconych rozgrywek i rozpatrzyć strategię ich obsługi (np. zapis do bazy danych w przypadku dłuższej nieaktywności, z możliwością ponownego powołania GameServer danej rozgrywki do życia). Dostępne rozgrywki są udostępniane graczowi z użyciem `LobbyMatchesAPI`.
- LobbyServer powinien zawierać również serwis obsługujący raportowanie wyników gier do leaderboardów. `LobbyLeaderboardsAPI` dostępne dla klienta pozwala jedynie na ich odczyt.

### Ogólne założenia dotyczące sieciowania
- Zalecany protokół sieciowy: TCP, lub przynajmniej Reliable UDP. Turowy charakter rozgrywki sprawia, że jest wrażliwa na stratę pakietów.
- Dalsze punkty zakładają komunikację sieciową za pomocą stworzonego na potrzeby gry schematu RPC. Szczegóły jego implementacji wykraczają poza zakres zadania.

### GameServer
Servery rozgrywki autorytatywnej będą składać się z kilku komponentów:
   -  `GameplayRuntime` - przechowuje aktualny stan rozgrywki i podlega replikacji do klienta.
   -  `PlayerAPI` - umożliwia graczom modyfikacje stanu rozgrywki, zgodnie z logiką gry. W przypadku rozgrywki PvP, API jest konsumowane przez 2 połączonych graczy. W przypadku rozgrywki PvE, jednego z graczy zastępuje osobny komponent. Używa `GameplayRuntime`, aby walidować wykonywane ruchy; w zależności od wymagań, może odpowiadać na nieprawidłowe żądania komunikatami błędu.
   -  `BotOpponentService` - używany w przypadku rozgrywki PvE; konsumuje `PlayerAPI` i wykonuje ruchy gracza komputerowego.
   -  `GameplayReplicationService` - reaguje na zmiany w `GameplayRuntime` i replikuje je do klienta.
 
### GameClient

Klient gry musi obsługiwać dwa połączenia: z LobbyServer oraz z GameServer. Zakładam użycie Unity.
- Połączenie z LobbyServer pozwala na obsługę matchmakingu oraz wyświetlanie leaderboardów.
- Lobby gry udostępnia widoki `IAvailableGamesView` oraz `ILeaderboardsView`. Zawierają się one we współdzielonej scenie **LobbyClient.unity**. Pozwalają one na interakcje z LobbyServer w granicach `LobbyMatchesAPI` oraz `LobbyLeaderboardsAPI`.
- Rozgrywka obsługiwana jest przez osobną, ładowaną adytywnie scenę: **GameClient.unity**. Pozwala na interkcje z grą w granicach `PlayerAPI`. Używa replikowanego z GameServer `GameplayRuntime`, aby przedstawić aktualny stan rozgrywki za pomocą `IGameplayView`.
- Zmiany stanu gry są requestowane przez graczy za pomocą podlegających lokalnej walidacji komend sieciowych, zgodnie z konwencją `SendFooRequest`. Odpowiedzi otrzymywane są również w formie komend sieciowych, w konwecji `ReceiveFooResponse`.
- Aby zminimalizować wpływ jakości połączenia w trybie PvE, możemy obłużyć go inaczej po stronie klienta: nie czekać na odpowiedź serwera dotyczącą legalności ruchu, tylko przeprowadzić go od razu. Jeśli serwer odpowie komunikatem błędu, cofamy ruch; w przeciwnym razie, otrzymamy ruch gracza AI i gra toczy się dalej.

<img width="800" height="712" alt="amrt" src="https://github.com/user-attachments/assets/21b83b50-6602-4905-b1b1-6c09672ef0bc" />
