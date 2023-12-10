import React from 'react';
import '../../../css/App.css';

import { Routes, Route, useNavigate, useLocation } from 'react-router-dom'
import { getFiltredRoutes } from '../../index'
import AppStorage from '../../../db/app-storage';

function App() {
  const navigate = useNavigate();
  const location = useLocation();
  const [logged, setLogged] = React.useState<boolean>(false);
  const storage = AppStorage();

  React.useEffect(() => {
    const token = storage.get('auth.token', undefined);
    if (!token && !/\/login|\/code|\/forgotten/g.test(location.pathname)) {
      navigate('/login')
      window.location.reload();
    }
    setLogged(!!token);
  }, [logged])


  return (
    <div className='w-full h-full'>
      <Routes>
        {
          getFiltredRoutes(logged)
            .map(route => (
              <Route 
                key={route.name} 
                index={route.index} 
                Component={route.element}
                path={route.path}
              />
            ))
        }
      </Routes>
    </div>
  );
}

export default App;
