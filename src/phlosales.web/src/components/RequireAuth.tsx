import { FC } from "react";
import { Navigate, Outlet, useLocation } from "react-router-dom"; 
import { useApp } from "../context";

export const RequireAuth  = () => {
  const { auth } = useApp();
  const location = useLocation();

  return auth?.isAuthenticated ? (
    <Outlet />
  ) : (
    <Navigate to="/login" state={{ from: location }} replace />
  );
};


