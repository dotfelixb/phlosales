import React from "react";

export interface IViewProps {
  children?: React.ReactNode;
}

export interface IAuth {
  accessToken: string;
  isAuthenticated: boolean;
}
export interface IAppContext {
  auth: IAuth;
  setAuth: React.Dispatch<React.SetStateAction<IAuth>>;
}

export interface ICustomer {
  id: number;
  name: string;
}

export interface IProduct {
  id: number;
  name: string;
}

export interface ISelectedItem {
  value: string;
  label: string;
}

export interface ISalesOrderList {
  id: number;
  customerId: number;
  customer: string;
  productId: number;
  product: string;
  price: number;
}

export interface ISalesUnit  {
  productId:  number;
  product:string;
  unit: number;
  gross:  number;
  highestPrice: number;
  lowestPrice:  number;
}

export interface ILoginResult {
  accessToken: string;
}
