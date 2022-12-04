import React, { FC, useEffect, useState } from "react";
import {
  Button,
  Card,
  Center,
  Group,
  Loader,
  Select,
  Table,
  Text,
  TextInput,
} from "@mantine/core";
import { useStyles } from "../styles";
import { useForm } from "@mantine/form";
import { randomId } from "@mantine/hooks";
import { IconPlus, IconX } from "@tabler/icons";
import { useAxiosPrivate } from "../hooks";
import { ICustomer, IProduct, ISelectedItem } from "../interface";
import { showNotification } from "@mantine/notifications";
import { useNavigate } from "react-router-dom";

interface INewOrderPage {}

export const NewOrderPage: FC<INewOrderPage> = () => {
  const axiosPrivate = useAxiosPrivate();
  const navigate = useNavigate();
  const { classes } = useStyles();
  const [customerList, setCustomerList] = useState<ISelectedItem[]>([]);
  const [productList, setProductList] = useState<ISelectedItem[]>([]);
  const [saving, setSaving] = useState(false);
  const form = useForm({
    initialValues: {
      request: [
        {
          customerId: 0,
          productId: 0,
          price: 0,
          key: randomId(),
        },
      ],
    },
    validate: {
      request: {
        customerId: (value) => (value < 1 ? "Customer is required" : null),
        productId: (value) => (value < 1 ? "Product is required" : null),
        price: (value) => (value < 1 ? "Price is required" : null),
      },
    },
  });

  const addOrder = () => {
    form.insertListItem("request", {
      customerId: 0,
      productId: 0,
      price: 0,
      key: randomId(),
    });
  };

  const removeOrder = (index: number) => {
    form.removeListItem("request", index);
  };

  const onCreateCustomer = (query: string) => {
    const createNew = async (): Promise<number> => {
      const response = await axiosPrivate.post(
        "methods/customer.create",
        JSON.stringify({ name: query })
      );

      if (response.status !== 201) {
        const err = "error";
        throw err;
      }

      return response?.data?.id;
    };

    let item: ISelectedItem = null!;

    createNew().then((r) => {
      item = { value: r.toString(), label: query };
      setCustomerList([...customerList, item]);
    });

    return item;
  };

  const onCreateProduct = (query: string) => {
    const createNew = async (): Promise<number> => {
      const response = await axiosPrivate.post(
        "methods/product.create",
        JSON.stringify({ name: query })
      );

      if (response.status !== 201) {
        const err = "error";
        throw err;
      }

      return response?.data?.id;
    };

    let item: ISelectedItem = null!;

    createNew().then((r) => {
      item = { value: r.toString(), label: query };
      setProductList([...productList, item]);
    });

    return item;
  };

  const handleSubmit = async () => {
    if (saving) {
      return;
    }

    const validation = form.validate();

    if (validation.hasErrors) {
      return;
    }
    setSaving(true);

    const values = JSON.stringify(form.values.request);
    const response = await axiosPrivate.post(
      "methods/salesorder.create",
      values
    );

    setSaving(false);

    if (response.status === 201) {
      navigate("/orders", { replace: true });
    } else {
      showNotification({
        message: "Not able to persist sales order, please try again!",
      });
    }
  };

  const getCustomers = async () => {
    const response = await axiosPrivate.get("methods/customer.list");
    const result = response?.data;
    if (response.status === 200) {
      setCustomerList(
        result?.customers.map((c: ICustomer) => ({
          value: c.id,
          label: c.name,
        }))
      );
    }
  };

  const getProducts = async () => {
    const response = await axiosPrivate.get("methods/product.list");
    const result = response?.data;
    if (response.status === 200) {
      setProductList(
        result?.products.map((p: IProduct) => ({
          value: p.id,
          label: p.name,
        }))
      );
    }
  };
  
  useEffect(() => {
    getCustomers();
    getProducts();

    // eslint-disable-next-line
  }, []);

  return (
    <div className={classes.pageView}>
      <header>
        <Group position="apart" my="lg">
          <Group position="left">
            <Text weight="bold" size={24}>
              New Sales Order
            </Text>
          </Group>
          <Group position="right">
            <Button color="indigo" onClick={handleSubmit}>
              {saving ? (
                <Center>
                  <Loader color="#fff" size="xs" />
                </Center>
              ) : (
                <span>Submit</span>
              )}
            </Button>
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
          {/* sx={{ tableLayout: "fixed" }} */}
          <Table>
            <thead>
              <tr className={classes.tableHeader}>
                <th>Ln #</th>
                <th>Customer</th>
                <th>Product</th>
                <th>Price</th>
                <th>
                  <Button size="xs" color="indigo" onClick={addOrder}>
                    <IconPlus stroke={3} size={16} />
                  </Button>
                </th>
              </tr>
            </thead>
            <tbody>
              {form.values.request.map((r, index) => {
                return (
                  <tr key={r.key}>
                    <td>
                      <Text>{index + 1}</Text>
                    </td>
                    <td>
                      <Select
                        data={customerList}
                        nothingFound="Nothing found"
                        searchable
                        creatable
                        getCreateLabel={(query) => `+ Create ${query}`}
                        onCreate={(query) => onCreateCustomer(query)}
                        styles={(theme) => ({
                          input: classes.input,
                          item: classes.selected,
                        })}
                        {...form.getInputProps(`request.${index}.customerId`)}
                      />
                    </td>
                    <td>
                      <Select
                        data={productList}
                        nothingFound="Nothing found"
                        searchable
                        creatable
                        getCreateLabel={(query) => `+ Create ${query}`}
                        onCreate={(query) => onCreateProduct(query)}
                        styles={(theme) => ({
                          input: classes.input,
                          item: classes.selected,
                        })}
                        {...form.getInputProps(`request.${index}.productId`)}
                      />
                    </td>
                    <td>
                      <TextInput
                        type="number"
                        {...form.getInputProps(`request.${index}.price`)}
                      />
                    </td>
                    <td>
                      <Button
                        variant="light"
                        size="xs"
                        color="red"
                        onClick={() => removeOrder(index)}
                      >
                        <IconX stroke={2} size={16} />
                      </Button>
                    </td>
                  </tr>
                );
              })}
            </tbody>
          </Table>
        </Card>
      </div>
    </div>
  );
};
