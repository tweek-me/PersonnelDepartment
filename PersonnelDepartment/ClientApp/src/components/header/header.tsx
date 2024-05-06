import { AppBar, Box, Button } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { Links } from "../../tools/constants/links";
import { NeverUnreachable } from "../../tools/errors/neverUnreachable";

type Page = 'Работники' | 'Отделы' | 'Договора';

export function Header() {
    const pages: Page[] = ['Работники', 'Отделы', 'Договора'];
    const navigation = useNavigate();

    function openPage(page: Page) {
        switch (page) {
            case "Работники": navigation(Links.employees); break;
            case "Отделы": navigation(Links.departments); break;
            case "Договора": navigation(Links.contracts); break;
            default: throw new NeverUnreachable(page);
        }
    }

    return (
        <AppBar position="static" color="info" sx={{ height: 40 }}>
            <Box sx={{ display: 'flex', flexGrow: 1, gap: 5 }}>
                {
                    pages.map(page => (
                        <Button
                            key={page}
                            variant="outlined"
                            onClick={() => openPage(page)}
                            sx={{ color: "#FFF", backgroundColor: 'transparent' }}
                        >
                            {page}
                        </Button>
                    ))
                }
            </Box>
        </AppBar>
    )
}