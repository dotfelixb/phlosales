FROM node:18.9.0-alpine3.15  as build
EXPOSE 80
WORKDIR /app

ENV PATH /app/node_modules/.bin:$PATH
ENV GENERATE_SOURCEMAP=false

# install dependencies
COPY src/phlosales.web/package.json ./
COPY src/phlosales.web/package-lock.json ./
RUN npm install --silent
RUN npm install react-scripts@5.0.1 -g --silent

COPY src/phlosales.web/ ./
RUN npm run build

# production environment
FROM nginx:stable-alpine
COPY --from=build /app/build /usr/share/nginx/html
COPY src/phlosales.web/nginx/nginx.conf /etc/nginx/conf.d/default.conf
CMD ["nginx", "-g", "daemon off;"]