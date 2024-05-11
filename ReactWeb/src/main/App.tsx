import { BrowserRouter, Routes, Route } from 'react-router-dom';
import HouseList from '../house/HouseList';
import './App.css'
import Header from './Header'
import HouseDetail from '../house/HouseDetail';

function App() {
  return (
    <BrowserRouter>
    <div className="container">
      <Header subtitle='Providing house all over the world!' />
      <Routes>
        <Route path="/" element={<HouseList />}></Route>
        <Route path='/houses/:id' element={<HouseDetail />}></Route>
      </Routes>
    </div>
    </BrowserRouter>
  );
}

export default App
