rmdir ./dist/EasyPark.Frontend  /s /q

npm run build

copy Dockerfile ./dist/Dockerfile
copy nginx.conf ./dist/nginx.conf

cd dist

docker build . -t hantse/bo-cow-app:dev
docker push hantse/bo-cow-app:dev