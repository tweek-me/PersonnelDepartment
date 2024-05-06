import { Box } from "@mui/material";
import { Header } from "../components/header/header";

export function Home() {
    return (
        <Box display={'flex'} width={'100%'} sx={{ width: '100%' }}>
            <Header />
        </Box>
    )
}