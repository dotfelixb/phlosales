import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { MantineProvider } from '@mantine/core';
import { NotificationsProvider } from '@mantine/notifications';
import { BrowserRouter } from 'react-router-dom';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <React.StrictMode>
    <MantineProvider
      withGlobalStyles
      withNormalizeCSS
      theme={{
        fontFamily: "Fredoka, Segoe UI, sans-serif",
        components: {
          Input: {
            styles: (theme) => ({
              input: {
                "&:focus": {
                  borderColor: theme.colors.indigo[5],
                },
              },
            }),
          },
        },
      }}
    >
      <NotificationsProvider position="top-right" zIndex={2077}>
          <BrowserRouter>
            <App />
          </BrowserRouter>
      </NotificationsProvider>
    </MantineProvider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
