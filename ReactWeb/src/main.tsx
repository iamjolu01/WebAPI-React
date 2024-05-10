import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './main/App.tsx'
import "bootstrap/dist/css/bootstrap.min.css"
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'

const queryClient = new QueryClient();
ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <QueryClientProvider client={queryClient}>
      <App />
    </QueryClientProvider>
  </React.StrictMode>,
)
