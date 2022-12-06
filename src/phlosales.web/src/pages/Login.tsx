import {
  Card,
  TextInput,
  PasswordInput,
  Group,
  Button,
  Text,
} from "@mantine/core";
import { useForm } from "@mantine/form";
import { IconEyeOff, IconEyeCheck } from "@tabler/icons";
import { FC } from "react";
import { useNavigate } from "react-router-dom";
import { useApp } from "../context";
import axios from "../context/axios";
import { ILoginResult } from "../interface";
import { useStyles } from "../styles";

interface ILoginPage {}

export const LoginPage: FC<ILoginPage> = () => {
  const navigate = useNavigate();
  const { classes } = useStyles();
  const { setAuth } = useApp();

  const form = useForm({
    initialValues: { username: "", password: "" },
    validate: {
      username: (value) =>
        value.length < 1 ? "Email must have at least a letter" : null,
      password: (value) =>
        value.length < 1 ? "Password must have at least a letter" : null,
    },
  });

  const handleSubmit = async (values: any) => {
    const body = JSON.stringify({
      username: values.username,
      password: values.password,
    });

    const response = await axios.post("auth/user.token", body);
    const result: ILoginResult = response?.data;

    if (response.status === 200) {
      setAuth({
        accessToken: result.accessToken,
        isAuthenticated: true,
      });
      navigate("/", { replace: true });
    }
  };

  return (
    <>
      <div
        data-id="kal-container"
        className={classes.container}
        style={{ flexDirection: "column" }}
      >
        <div style={{ textAlign: "center", padding: "44px" }}>
          <Text weight="bold" size={32}>
            Login
          </Text>
        </div>
        <div data-id="kal-main" className={classes.loginMain}>
          <div style={{ flex: 1 }}></div>
          <div style={{ flex: "none", width: 380 }}>
            <form onSubmit={form.onSubmit(handleSubmit)}>
              <Card
                withBorder
                radius="sm"
                className={classes.card}
                px={24}
                py={34}
              >
                <TextInput
                  styles={() => ({
                    input: classes.input,
                  })}
                  label="Email"
                  {...form.getInputProps("username")}
                />
                <PasswordInput
                  styles={() => ({
                    input: classes.input,
                  })}
                  mt="md"
                  label="Password"
                  {...form.getInputProps("password")}
                  visibilityToggleIcon={({ reveal, size }) =>
                    reveal ? (
                      <IconEyeOff size={size} />
                    ) : (
                      <IconEyeCheck size={size} />
                    )
                  }
                />

                <Group mt={44}>
                  <Button color="indigo" type="submit" fullWidth size="md">
                    Login
                  </Button>
                </Group>
              </Card>
            </form>
          </div>
          <div style={{ flex: 1 }}></div>
        </div>
      </div>
    </>
  );
};
