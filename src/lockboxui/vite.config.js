import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import mkcert from "vite-plugin-mkcert";

// https://vitejs.dev/config/
export default defineConfig({
	base: "/",
	build: {
		outDir: "../Lockbox.Web/wwwroot/client",
		emptyOutDir: true,
	},
	server: {
		https: true,
		port: 6363,
	},
	plugins: [react(), mkcert()],
});
