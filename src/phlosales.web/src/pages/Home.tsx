import {
  Breadcrumbs,
  Card,
  Center,
  Grid,
  Group,
  Loader,
  Table,
  Text,
} from "@mantine/core";
import { FC, useEffect, useState } from "react";
import { useAxiosPrivate } from "../hooks";
import { ISalesUnit } from "../interface";
import { useStyles } from "../styles";

interface IHomePage {}

export const HomePage: FC<IHomePage> = () => {
  const axiosPrivate = useAxiosPrivate();
  const [sellingUnits, setSellingUnits] = useState<ISalesUnit[]>([]);
  const [grossingUnits, setGrossingUnits] = useState<ISalesUnit[]>([]);
  const [orderedUnits, setOrderedUnits] = useState<ISalesUnit[]>([]);
  const [loadSelling, setLoadSelling] = useState(false);
  const [loadGrossing, setLoadGrossing] = useState(false);
  const [loadOrdered, setLoadOrdered] = useState(false);
  const { classes } = useStyles();

  const getSellingUnit = async () => {
    setLoadSelling(true);
    const response = await axiosPrivate.get("sales/dashboard.selling");

    if (response.status === 200) {
      setSellingUnits(response?.data?.units);
    }
    setLoadSelling(false);
  };

  const getGrossingUnit = async () => {
    setLoadGrossing(true);
    const response = await axiosPrivate.get("sales/dashboard.grossing");

    if (response.status === 200) {
      setGrossingUnits(response?.data?.units);
    }
    setLoadGrossing(false);
  };

  const getOrderedUnit = async () => {
    setLoadOrdered(true);
    const response = await axiosPrivate.get("sales/dashboard.ordered");

    if (response.status === 200) {
      setOrderedUnits(response?.data?.units);
    }
    setLoadOrdered(false);
  };

  useEffect(() => {
    getSellingUnit();
    getGrossingUnit();
    getOrderedUnit();

    // eslint-disable-next-line
  }, []);

  const sellingRows = sellingUnits.map((u) => (
    <tr key={u.productId}>
      <td>{u.product}</td>
      <td>{u.unit}</td>
      <td>
        {u.gross.toLocaleString("en-US", {
          style: "currency",
          currency: "usd",
        })}
      </td>
    </tr>
  ));

  const grossingRows = grossingUnits.map((u) => (
    <tr key={u.productId}>
      <td>{u.product}</td>
      <td>{u.unit}</td>
      <td>
        {u.gross.toLocaleString("en-US", {
          style: "currency",
          currency: "usd",
        })}
      </td>
    </tr>
  ));

  const orderedRows = orderedUnits.map((u) => (
    <tr key={u.productId}>
      <td>{u.product}</td>
      <td>{u.unit}</td>
      <td>
        {u.gross.toLocaleString("en-US", {
          style: "currency",
          currency: "usd",
        })}
      </td>
      <td>
        {u.lowestPrice.toLocaleString("en-US", {
          style: "currency",
          currency: "usd",
        })}
      </td>
      <td>
        {u.highestPrice.toLocaleString("en-US", {
          style: "currency",
          currency: "usd",
        })}
      </td>
    </tr>
  ));

  const items = [{ title: "Dashboard", href: "/" }].map((item, index) => (
    <span key={index} style={{ cursor: "pointer", fontSize: 12 }}>
      {item.title}
    </span>
  ));

  return (
    <div className={classes.pageView}>
      <header>
        <div>
          <div>
            <Breadcrumbs>{items}</Breadcrumbs>
          </div>
        </div>
        <Group position="apart" my="lg">
          <Group position="left">
            <Text weight="bold" size={24}>
              Dashboard
            </Text>
          </Group>
        </Group>
      </header>
      <div>
        <Grid>
          <Grid.Col md={6} lg={6}>
            <Card
              withBorder
              radius="sm"
              className={classes.tableCard}
              my="lg"
              p={0}
            >
              <Card.Section p="xs" className={classes.cardSection}>
                <Text weight={600}>Highest Selling Unit</Text>
              </Card.Section>

              <Table sx={{ tableLayout: "fixed" }}>
                <thead>
                  <tr className={classes.tableHeader}>
                    <th>Product</th>
                    <th>Units Sold</th>
                    <th>Total Gross</th>
                  </tr>
                </thead>
                <tbody>
                  {sellingUnits.length > 0 ? (
                    sellingRows
                  ) : (
                    <tr>
                      <td colSpan={2}>
                        {loadSelling ? (
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
          </Grid.Col>

          <Grid.Col md={6} lg={6}>
            <Card
              withBorder
              radius="sm"
              className={classes.tableCard}
              my="lg"
              p={0}
            >
              <Card.Section p="xs" className={classes.cardSection}>
                <Text weight={600}>Highest Grossing Unit</Text>
              </Card.Section>

              <Table sx={{ tableLayout: "fixed" }}>
                <thead>
                  <tr className={classes.tableHeader}>
                    <th>Product</th>
                    <th>Units Sold</th>
                    <th>Total Gross</th>
                  </tr>
                </thead>
                <tbody>
                  {grossingRows.length > 0 ? (
                    grossingRows
                  ) : (
                    <tr>
                      <td colSpan={3}>
                        {loadGrossing ? (
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
          </Grid.Col>

          <Grid.Col md={12} lg={12}>
            <Card
              withBorder
              radius="sm"
              className={classes.tableCard}
              my="lg"
              p={0}
            >
              <Card.Section p="xs" className={classes.cardSection}>
                <Text weight={600}>Ordered Unit (High and Low)</Text>
              </Card.Section>

              <Table sx={{ tableLayout: "fixed" }}>
                <thead>
                  <tr className={classes.tableHeader}>
                    <th>Product</th>
                    <th>Units Sold</th>
                    <th>Total Gross</th>
                    <th>Lowest Selling Price</th>
                    <th>Highest Selling Price</th>
                  </tr>
                </thead>
                <tbody>
                  {orderedRows.length > 0 ? (
                    orderedRows
                  ) : (
                    <tr>
                      <td colSpan={5}>
                        {loadOrdered ? (
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
          </Grid.Col>
        </Grid>
      </div>
    </div>
  );
};
