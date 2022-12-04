export interface IViewProps {
  children?: React.ReactNode;
}

export interface IAppContext {}

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
