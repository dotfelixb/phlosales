import { FC } from "react";
import { TablerIcon } from "@tabler/icons";
import { Center, createStyles, Tooltip } from "@mantine/core";
import { Link, useLocation } from "react-router-dom";

const styles = createStyles((theme) => ({
  navbarLink: {
    color: theme.colors.dark[3],
    cursor: "pointer",
    padding: "12px 16px",
    textDecoration: "none",
    borderRadius: "4px",
    "&:hover": {
      backgroundColor: theme.colors.indigo[2],
      color: theme.white,
    },
  },
  navbarAddBar: { 
    backgroundColor: theme.colors.indigo[5],
    color: theme.white,
  },
  navbarRemoveBar: {
    backgroundColor: "transparent",
    color: theme.colors.dark[3],
  },
}));

interface INavbarLink {
  icon: TablerIcon;
  label: string;
  to: string;
}

export const NavbarLink: FC<INavbarLink> = ({ icon: Icon, label, to }) => {
  const { classes, cx } = styles();
  const location = useLocation();
  const active = location.pathname.split("/")[1] === to;
  const activeStyle = active ? classes.navbarAddBar : classes.navbarRemoveBar;

  return (
    <Tooltip label={label} position="right" transitionDuration={0}>
      <Link to={to} style={{ textDecoration: "none" }}>
        <Center data-info={active}>
          <div className={cx(classes.navbarLink, activeStyle)}>
            <Icon stroke={1.5} />
          </div>
        </Center>
      </Link>
    </Tooltip>
  );
};
