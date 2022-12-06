FROM nginx:stable-alpine
EXPOSE 5000

COPY src/nginx/nginx.conf /etc/nginx/nginx.conf
CMD ["nginx", "-g", "daemon off;"]