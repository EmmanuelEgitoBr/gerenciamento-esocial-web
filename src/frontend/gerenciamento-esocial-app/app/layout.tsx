import type { Metadata } from "next";
import { AuthProvider } from "@/context/AuthProvider";
import "./globals.css";

export const metadata: Metadata = {
  title: "Gerenciamento e-Social",
  description: "App de gerenciamento e-Social",
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="pt-BR">
      <head>
        <link
          rel="stylesheet"
          href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css"
        />
      </head>
      <body>
        <AuthProvider>
          {children}
        </AuthProvider>
      </body>
    </html>
  );
}
