# PLRadioTask
Aplikacja ta to modułowy system CMS stworzony w technologii ASP.NET Core, z podziałem na warstwy: Api, Application, Domain, Infrastructure. Projekt wspiera CRUD dla artykułów i kategorii, statystyki publikacji oraz posiada testy jednostkowe.

Struktura katalogów:
PLRadioTask/
├── Api/               # Warstwa prezentacji (API REST) + Dockerfile
├── Application/       # DTO, serwisy, interfejsy, walidatory
├── Domain/            # Modele domenowe, enumy, logika biznesowa
├── Infrastructure/    # EF Core DbContext, repozytoria
├── Tests/             # Testy jednostkowe (Xunit + Moq)
├── Makefile           # Skrypty budowania i uruchamiania
├── PLRadioTask.sln

Uruchomienie lokalne

Przywróć zależności:

dotnet restore

Zbuduj projekt:

make build

Uruchom API lokalnie.
make run


Przykładowe zapytania curl

Dodanie artykułu
curl -X POST http://localhost:5202/api/articles \
  -H "Content-Type: application/json" \
  -d '{
        "title": "Nowy Artykuł",
        "content": "Zawartość",
        "author": "Admin",
        "categoryId": "WSTAW_GUID_KATEGORII"
      }'

Pobierz statystyki
curl http://localhost:5202/api/articles/statistics

Dodaj kategorię
curl -X POST http://localhost:5202/api/categories \
  -H "Content-Type: application/json" \
  -d '"Technologia"'

  