# Ścieżki
API_PROJECT=Api/Api.csproj
TEST_PROJECT=Tests/Tests.csproj

# Budowanie
build:
	dotnet build $(API_PROJECT)

#Testowanie
test:
	dotnet test $(TEST_PROJECT)

#Uruchamianie aplikacji lokalnie
run:
	dotnet run --project $(API_PROJECT)

#Docker: budowanie obrazu
docker-build:
	docker build -t cms-api -f Api/Dockerfile .

#Docker: uruchamianie kontenera
docker-run:
	docker run -p 5000:80 --name cms-api-container cms-api

#Czyszczenie
clean:
	dotnet clean
