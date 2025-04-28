FROM node:18 AS build
WORKDIR /app
COPY src/Sync.Web/package.json src/Sync.Web/package-lock.json ./
RUN npm install
COPY src/Sync.Web ./
RUN npm run build

FROM nginx:alpine
COPY --from=build /app/build /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]