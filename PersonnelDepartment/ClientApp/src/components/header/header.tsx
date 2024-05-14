import HomeIcon from '@mui/icons-material/Home';
import { AppBar, Box, Button, IconButton } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { Links } from "../../tools/constants/links";
import { NeverUnreachable } from "../../tools/errors/neverUnreachable";

type Page = 'Домой' | 'Работники' | 'Отделы' | 'Договора';

export function Header() {
    const pages: Page[] = ['Работники', 'Отделы', 'Договора'];
    const navigation = useNavigate();

    function openPage(page: Page) {
        switch (page) {
            case "Домой": navigation(Links.home); break;
            case "Работники": navigation(Links.employees); break;
            case "Отделы": navigation(Links.departments); break;
            case "Договора": navigation(Links.contracts); break;
            default: throw new NeverUnreachable(page);
        }
    }

    return (
        <AppBar color="info" sx={{ height: 40 }}>
            <Box sx={{ display: 'flex', flexGrow: 1, gap: 5, marginLeft: 5 }}>
                <IconButton onClick={() => openPage('Домой')}>
                    <HomeIcon sx={{ color: '#FFF' }} />
                </IconButton>
                {
                    pages.map(page => (
                        <Button
                            key={page}
                            variant="text"
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