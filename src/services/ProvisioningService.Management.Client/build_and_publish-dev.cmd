rmdir publish  /s /q
dotnet publish -c release -o publish

cd publish
docker build . -t hantse/provisionning-service:dev
docker push hantse/provisionning-service:dev