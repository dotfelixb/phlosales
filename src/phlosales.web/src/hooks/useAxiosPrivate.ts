import { useEffect } from "react";
import { axiosPrivate, useApp } from "../context"; 

export const useAxiosPrivate = () => { 
  const { auth } = useApp();

  useEffect(() => {
    const requestIntercept = axiosPrivate.interceptors.request.use(
      (config: any) => {
        if (config === undefined) return;

        if (!config.headers["Authorization"]) {
          config.headers["Authorization"] = `Bearer ${auth.accessToken}`;
        }

        return config;
      },
      (error) => Promise.reject(error)
    );
    return () => {
      axiosPrivate.interceptors.request.eject(requestIntercept);
    };
  }, [auth]);
  return axiosPrivate;
};
