import { Loader, Text } from "@mantine/core";
import React, { FC } from "react";

interface ILoading {
  message: string;
  opacity?: number;
}

export const Loading: FC<ILoading> = ({ message, opacity = 0.85 }) => {
  return (
    <div
      style={{
        height: "100vh",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        left: 0,
        top: 0,
        right: 0,
        border: 0,
        position: "fixed",
        backgroundColor: "white",
        zIndex: 1000,
        opacity,
      }}
    >
      <div
        style={{
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          justifyContent: "center",
        }}
      >
        <Loader color="indigo" />
        <Text mt="lg" weight="bold">
          {message}
        </Text>
      </div>
    </div>
  );
};
