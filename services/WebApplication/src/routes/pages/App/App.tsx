import React from 'react';
import '../../../css/App.css';

import { Routes, Route, useNavigate, useLocation } from 'react-router-dom'
import { registredRoutes } from '../../index'
import AppStorage from '../../../db/app-storage';
import { IRoutePath } from '../../../interfaces/IRoute';

function App() {
  const navigate = useNavigate();
  const location = useLocation();
  const [logged, setLogged] = React.useState<boolean>(false);
  const storage = AppStorage();

  React.useEffect(() => {
    const { token, expire } = storage.get('auth', { token: undefined, expire: undefined });

    if (!token && !/\/login|\/code|\/forgotten/g.test(location.pathname)) {
      navigate('/login')
      window.location.reload();
    }

    if (new Date() >= new Date(expire)) {
      storage.clear();
      window.location.reload();
    }

    setLogged(!!token);
  }, [logged])

  const mapRoute = (route: IRoutePath): JSX.Element => (<Route 
    key={route.path} 
    index={route.main} 
    Component={route.element}
    path={route.path}
  />)

  return (
    <div className='w-full h-full'>
      <Routes>
        {registredRoutes.map((route: IRoutePath) => mapRoute(route))}
      </Routes>
    </div>
  );
}

export default App;
