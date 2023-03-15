# Forum

Demo projektu Forum dyskusyjnego

Dane do logowania dla testowego profilu:
login: demo@gmail.com
hasło: demo

Baza danych: MYSQL
ORM: Dapper

CO ZOSTAŁO WPROWADZONE LUB CZĘŚCIOWO DODANE

LOGOWANIE i AUTORYZACJA:
- Autentykacja cookies przy logowaniu + persistent cookies przy opcji remember me
- Przekierowanie na ostatnio odwiedzaną stronę jeśli przejdziemy do logowania z poziomu wnętrza wątku (w innych przypadkach przeniesie na stronę główną)

STRONA GŁÓWNA:
- Ładowanie z bazy danych ostatnich 10 postów
- Linkowanie bezpośrednio do profilu autora
- Zaciąganie aktywnych kategorii do menu bocznego
- Ładowanie najciekawszych wątków z ostatniego czasu = kwerenda pobiera je biorąc pod uwagę dzień dodania (segreguje po najnowszym), 
a w następnej kolejności po ilości komentarzy

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

REJESTRACJA:
- Adres e-mail i nazwa użytkownika muszą być unikatowe podczas zakładania
- Wprowadzona minimalna walidacja formularza rejestracji z poziomu page models

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
- Dodane kilka wstępnych Policies jednak jeszcze nie używane

FRONT-END:
- Bootstrap 5.0 (stronie nie jest jeszcze mobile-friendly)

Póki co zarówno dodawanie wątków jak i komentarzy jest dostępne dla użytkowników których IsActive w bazie danych = false
(niby zbanowani ;) ) jednak w przypadku komentarza jego treść się nie wyświetli.

Aplikacja jak najmocniej stara się opierać o wzorzec repozytoriów.
