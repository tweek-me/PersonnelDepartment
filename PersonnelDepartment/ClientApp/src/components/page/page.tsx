import { Box } from "@mui/material";
import { PropsWithChildren } from "react";
import { Header } from "../header/header";

export function Page(props: PropsWithChildren) {
    return (
        <Box margin={0}>
            <Header />
            {props.children}
        </Box>
    )
}