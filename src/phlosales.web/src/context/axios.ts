import axios from "axios"

const BASEURL =
window.location.hostname === "localhost"
  ? "http://localhost:5000/"
  : `${window.location.origin}/`;

export default axios.create({
    baseURL: BASEURL
})

export const axiosPrivate = axios.create({
    baseURL: BASEURL,
    headers: {"content-type":"application/json"},
    withCredentials: true
})