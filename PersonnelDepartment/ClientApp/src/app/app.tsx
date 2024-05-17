import ReactDOM from 'react-dom/client';
import { Notifications } from '../components/notifications/notifications';
import { MainRouter } from './mainRouter';
import { Box } from '@mui/material';

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);

root.render(
    <Box>
        <Notifications />
        <MainRouter />
    </Box>
);

