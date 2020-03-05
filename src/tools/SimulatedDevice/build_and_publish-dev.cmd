rmdir publish  /s /q
dotnet publish -c release -o publish

cd publish
docker build . -t hantse/simulateddevice:dev
docker push hantse/simulateddevice:dev