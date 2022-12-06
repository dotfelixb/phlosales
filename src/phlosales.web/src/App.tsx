import { Routes, Route } from "react-router-dom";
import { Layout, RequireAuth } from "./components";
import {
  CustomersPage,
  HomePage,
  LoginPage,
  NewOrderPage,
  OrdersPage,
  ProductsPage,
} from "./pages";

function App() {
  return (
    <Routes>
      <Route element={<RequireAuth />}>
        <Route path="/" element={<Layout />}>
          <Route index element={<HomePage />} />
          <Route path="orders">
            <Route index element={<OrdersPage />} />
            <Route path="new" element={<NewOrderPage />} />
          </Route>
          <Route path="customers" element={<CustomersPage />} />
          <Route path="products" element={<ProductsPage />} />
        </Route>
      </Route>
      <Route path="/login" element={<LoginPage />} />
    </Routes>
  );
}

export default App;
