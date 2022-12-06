import { createContext, FC, useContext, useState } from "react";
import { IAppContext, IAuth, IViewProps } from "../interface";

export const AppContext = createContext<IAppContext>(null!);

export const useApp = () => useContext(AppContext);

export const AppProvider: FC<IViewProps> = ({ children }) => {
  const [auth, setAuth] = useState<IAuth>({
    accessToken: "",
    isAuthenticated: false,
  });
  return (
    <AppContext.Provider
      value={{
        auth,setAuth
      }}
    >
      {children}
    </AppContext.Provider>
  );
};
