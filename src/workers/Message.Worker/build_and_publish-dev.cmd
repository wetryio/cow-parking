rmdir publish  /s /q
dotnet publish -c release -o publish

cd publish
docker build . -t hantse/event-worker:dev
docker push hantse/event-worker:dev