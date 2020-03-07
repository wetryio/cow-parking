rmdir publish  /s /q
dotnet publish -c release -o publish

cd publish
docker build . -t hantse/entity-service:dev
docker push hantse/entity-service:dev