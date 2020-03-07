rmdir ./dist/debug-tool  /s /q

npm run build

copy Dockerfile ./dist/Dockerfile
copy nginx.conf ./dist/nginx.conf

cd dist

docker build . -t hantse/debug-tool-app:dev
docker push hantse/debug-tool-app:dev