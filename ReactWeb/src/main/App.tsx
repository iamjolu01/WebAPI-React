import { BrowserRouter, Routes, Route } from 'react-router-dom';
import HouseList from '../house/HouseList';
import './App.css'
import Header from './Header'
import HouseDetail from '../house/HouseDetail';
import HouseAdd from '../house/HouseAdd';
import HouseEdit from '../house/HouseEdit';

function App() {
  return (
    <BrowserRouter>
    <div className="container">
      <Header subtitle='Providing house all over the world!' />
      <Routes>
        <Route path="/" element={<HouseList />}></Route>
        <Route path='/houses/:id' element={<HouseDetail />}></Route>
        <Route path="/house/add" element={<HouseAdd />}></Route>
        <Route path="/house/edit/:id" element={<HouseEdit />}></Route>
      </Routes>
    </div>
    </BrowserRouter>
  );
}

export default App
