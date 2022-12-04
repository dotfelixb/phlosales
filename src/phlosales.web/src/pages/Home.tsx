import { Card, Grid, Group, Text } from "@mantine/core";
import React, { FC } from "react";
import { useStyles } from "../styles";

interface IHomePage {}

export const HomePage: FC<IHomePage> = () => {
  const { classes } = useStyles();
  return (
    <div className={classes.pageView}>
      <header>
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
            <Card withBorder radius="sm" className={classes.card} mb="lg">
              <Card.Section p="xs">
                <Text weight={500}>Hightest Selling Unit</Text>
              </Card.Section>
            </Card>
          </Grid.Col>

          <Grid.Col md={6} lg={6}>
            <Card withBorder radius="sm" className={classes.card} mb="lg">
              <Card.Section p="xs">
                <Text weight={500}>Hightest Grossing Unit</Text>
              </Card.Section>
            </Card>
          </Grid.Col>
        </Grid>
      </div>
    </div>
  );
};
