rmdir publish  /s /q
dotnet publish -c release -o publish

cd publish
docker build . -t hantse/debug-service:dev
docker push hantse/debug-service:dev