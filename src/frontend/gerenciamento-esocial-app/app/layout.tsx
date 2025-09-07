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
      <body>
        <AuthProvider>
          {children}
        </AuthProvider>
      </body>
    </html>
  );
}
