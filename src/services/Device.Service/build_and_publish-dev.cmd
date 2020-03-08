rmdir publish  /s /q
dotnet publish -c release -o publish

cd publish
docker build . -t hantse/device-service:dev
docker push hantse/device-service:dev