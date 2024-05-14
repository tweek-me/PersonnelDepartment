import ReactDOM from 'react-dom/client';
import { NotificationProvider } from '../components/notifications/notifications';
import { MainRouter } from './mainRouter';

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);

root.render(
    <>
        <NotificationProvider>
            <MainRouter />
        </NotificationProvider>
    </>
);

