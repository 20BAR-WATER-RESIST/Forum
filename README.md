# Forum

Demo projektu Forum dyskusyjnego:
- link1: https://forumdemo.toadres.pl/
- link2: http://demoapp.click

Dane do logowania dla testowego profilu: <br />
administrator:
login: demoadmin@gmail.com <br />
hasło: demoadmin

zwykły użytkownik:<br />
login: demo@gmail.com <br />
hasło: demo

TECHNOLOGIA: Asp .NET Core Razor Pages <br />
BAZA DANYCH: MYSQL <br />
ORM: Dapper <br />
DEPLOY: DOCKER + VPS mikr.us (lokalizacja serwerów: Finlandia) + Cloudlfare (druga domena)

CO ZOSTAŁO WPROWADZONE LUB CZĘŚCIOWO DODANE

STRONA GŁÓWNA:
- Ładowanie z bazy danych ostatnich 10 postów
- Linkowanie bezpośrednio do profilu autora
- Zaciąganie aktywnych kategorii do menu bocznego
- Ładowanie najciekawszych wątków z ostatniego czasu = kwerenda pobiera je biorąc pod uwagę dzień dodania (segreguje po najnowszym), 
a w następnej kolejności po ilości komentarzy

PANEL ADMINISTRACYJNY:
- Dostęp tylko dla zalogowanych administratorów
- Ładowanie wszystkich zgłoszeń z bazy danych
- Ładowanie zablokowanych użytkowników z bazy danych których okres blokady wkrótce się zakończy (7 dni od aktualnej daty)
- Kontrolki umożliwiające weryfikowanie / usuwanie / banowanie / anulowanie zbanowanych użytkowników

Kategorie:
- Ładowanie 10 wątków z danej kategorii poukładanych od najnowszej daty + paginacja do ładowania kolejnych
- Aktywna kategoria podświetla się pogrubioną czcionką
- Okienko z najciekawszymi wątkami pokazuje te z wybranej kategorii

PROFIL UŻYTKOWNIKA:
- Warstwa front-endowa
- Pobieranie nazwy użytkownika, typu konta, topiców i komentarzy które dodał od najnowszego
- Dodany odnośnik w komentarzu do wątku w którym został dodany
- Paginacja komentarzy (na samym dole wątku)
- Pierwsza walidacja czy użytkownik jest właścicielem konta (przycisk settings pokazuje się w profilu tylko właścicielowi konta)

LOGOWANIE i AUTORYZACJA:
- Autentykacja cookies przy logowaniu + persistent cookies przy opcji remember me
- Przekierowanie na ostatnio odwiedzaną stronę o ile nie logujemy się z poziomu widoku logowania (w tym przypadku przeniesie na stronę główną)

REJESTRACJA:
- Adres e-mail i nazwa użytkownika muszą być unikatowe podczas zakładania

WALIDACJE FORMULARZY:
- Każdy z formularzy na stronie jest walidowany na poprawność jego wypełnienia
- Formularz rejestracji posiada dodatkową walidację która sprawdza czy próbujesz zarejestrować użytkownika na już istniejący adres e-mail
- Formularz logowania posiada dodatkową walidację która sprawdza czy dane do logowania są poprawne a w przypadku błędu wyświetla odpowiedni komunikat

WYSZUKIWARKA:
- Wyszukiwanie wstępnie dodane dla frazy która znajduje się zarówno w tytule wątku jak i jego treści
- Walidacja czy użytkownik wprowadził znaki specjalne do pola wyszukiwania

DODAWANIE WĄTKU:
- Wymaga zalogowania
- Treść wątku ograniczone do 2000 znaków

DODAWANIE KOMENTARZA:
- Wymaga zalogowania
- Treść komentarza do 2000 znaków

AUTORYZACJE:
- Niektóre strony jak np panel administracyjny będą wymagały zalowania zanim wyświetlą widok (dane do logowania na samej górze)

FRONT-END:
- Bootstrap 5.0 (stronie nie jest jeszcze mobile-friendly)

Uwaga przy dodawaniu nowego wątku, komentarza lub dowolnej treści może nie zgadzać się czas dodania (ma to związek z inną strefą czasową na której stoi aplikacja - strefy czasowe jeszcze nie zostały skonfigurowane).

Póki co zarówno dodawanie wątków jak i komentarzy jest dostępne dla użytkowników których IsActive w bazie danych = false
(niby zbanowani ;) ) jednak w przypadku komentarza jego treść się nie wyświetli.

Aplikacja jak najmocniej stara się opierać o wzorzec repozytoriów.
