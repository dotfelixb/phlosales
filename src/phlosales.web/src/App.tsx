import React from "react";
import { Routes, Route } from "react-router-dom";
import { Layout } from "./components";
import { CustomersPage, HomePage, NewOrderPage, OrdersPage, ProductsPage } from "./pages";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route index element={<HomePage />} />
        <Route path="orders">
          <Route index element={<OrdersPage />} />
          <Route path="new" element={<NewOrderPage />} />
        </Route>
        <Route path="customers" element={<CustomersPage />} />
        <Route path="products" element={<ProductsPage />} />
      </Route>
    </Routes>
  );
}

export default App;
