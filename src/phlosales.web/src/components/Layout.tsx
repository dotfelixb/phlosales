import { createStyles } from "@mantine/core";
import {
  IconDiscount2,
  IconGauge,
  TablerIcon,
} from "@tabler/icons";
import React, { FC } from "react";
import { Outlet } from "react-router-dom";
import { NavbarLink } from "./NavbarLink";

interface ILayout {
  children?: React.ReactNode;
}

const styles = createStyles((theme) => ({
  container: {
    display: "flex",
    width: "100vw",
    height: "100vh",
    position: "relative",
    overflow: "hidden",
  },
  main: {
    display: "flex",
    flex: 1,
    flexDirection: "column",
    position: "relative",
    height: "100%",
  },
  sidebar: {
    width: "72px",
    display: "flex",
    flexDirection: "column",
    borderRightWidth: 1,
    borderRightStyle: "solid",
    borderRightColor: theme.colors.indigo[5],
    padding: theme.spacing.sm,
  },
}));

interface INavLink {
  icon: TablerIcon;
  label: string;
  to: string;
}

const links: INavLink[] = [
  { icon: IconGauge, label: "Dashboard", to: "" },
  { icon: IconDiscount2, label: "Sales Order", to: "orders" },
];

export const Layout: FC<ILayout> = ({ children }) => {
  const { classes } = styles();

  const navLinks = links.map((link, index) => (
    <NavbarLink key={index} {...link} />
  ));

  return (
    <div className={classes.container}>
      <div className={classes.sidebar}>
        <div style={{ flex: 1 }}>{navLinks}</div>
      </div>
      <div className={classes.main}>
        {children}
        <Outlet />
      </div>
    </div>
  );
};
