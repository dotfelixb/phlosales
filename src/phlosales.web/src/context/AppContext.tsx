import { createContext, FC, useContext } from "react";
import { IAppContext, IViewProps } from "../interface";

export const AppContext = createContext<IAppContext>(null!);

export const useApp = () => useContext(AppContext);

export const AppProvider: FC<IViewProps> = ({ children }) => {
  return <AppContext.Provider value={{}}>{children}</AppContext.Provider>;
};
