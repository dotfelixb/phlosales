import {
  Button,
  Card,
  Center,
  Group,
  Loader,
  Table,
  Text,
} from "@mantine/core";
import React, { FC, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Loading } from "../components";
import { useAxiosPrivate } from "../hooks";
import { ISalesOrderList } from "../interface";
import { useStyles } from "../styles";

interface IOrdersPage {}

export const OrdersPage: FC<IOrdersPage> = () => {
  const axiosPrivate = useAxiosPrivate();
  const [salesOrderList, setSalesOrderList] = useState<ISalesOrderList[]>([]);
  const [loading, setLoading] = useState(false);
  const { classes } = useStyles();

  useEffect(() => {
    const getSalesOrder = async () => {
      setLoading(true);
      const response = await axiosPrivate.get("methods/salesorder.list");
      const result = response?.data;

      if (response.status === 200) {
        setSalesOrderList(result?.salesOrders);
      }
      setLoading(false);
    };

    getSalesOrder();
  }, []);

  const rows = salesOrderList.map((s) => (
    <tr key={s.id}>
      <td>{s.id}</td>
      <td>{s.customer}</td>
      <td>{s.product}</td>
      <td>
        {s.price.toLocaleString("en-US", {
          style: "currency",
          currency: "usd",
        })}
      </td>
    </tr>
  ));

  return (
    <div className={classes.pageView}>
      <header>
        <Group position="apart" my="lg">
          <Group position="left">
            <Text weight="bold" size={24}>
              Sales Order
            </Text>
          </Group>
          <Group position="right">
            <Link to="new">
              <Button color="indigo">New Sales Order</Button>
            </Link>
          </Group>
        </Group>
      </header>
      <div>
        <Card
          withBorder
          radius="sm"
          className={classes.tableCard}
          my="lg"
          p={0}
        >
          <Table sx={{ tableLayout: "fixed" }}>
            <thead>
              <tr className={classes.tableHeader}>
                <th>Id</th>
                <th>Customer</th>
                <th>Product</th>
                <th>Price</th>
              </tr>
            </thead>
            <tbody>
              {salesOrderList.length > 0 ? (
                rows
              ) : (
                <tr>
                  <td colSpan={4}>
                    {loading ? (
                      <Center py="lg">
                        <Loader color="indigo" />
                      </Center>
                    ) : (
                      <Text weight={600} p="lg" align="center">
                        Nothing to see
                      </Text>
                    )}
                  </td>
                </tr>
              )}
            </tbody>
          </Table>
        </Card>
      </div>
    </div>
  );
};
