import { createStyles } from "@mantine/core";

export const useStyles = createStyles((theme) => ({
  pageView: {
    padding: theme.spacing.md,
  },
  tableCard: {
    overflow: "unset",
    borderColor: theme.colors.indigo[1],
  },
  tableHeader: {
    backgroundColor: "#F9FAFF",
  },
  input: {
    "&:focus": {
      borderColor: theme.colors.indigo[5],
    },
  },
  selected: {
    "&[data-selected]": {
      "&, &:hover": {
        backgroundColor: theme.colors.indigo[5],
        color: theme.white,
      },
    },
    "&[data-hovered]": {},
  },
  card: {
    overflow: "unset",
    backgroundColor: "#F9FAFF",
    borderColor: theme.colors.indigo[1],
  },
}));
