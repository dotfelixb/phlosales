import axios from "axios";

const BASEURL =
  window.location.hostname === "localhost" ||
  window.location.hostname === "127.0.0.1"
    ? "http://localhost:5000/"
    : `${window.location.origin}/`;

export default axios.create({
  baseURL: BASEURL,
  headers: { "content-type": "application/json" },
});

export const axiosPrivate = axios.create({
  baseURL: BASEURL,
  headers: { "content-type": "application/json" },
  withCredentials: true,
});
