const { createApp } = Vue;
import IncomesPage from './pages/IncomesPage.js';

const mounts = {
    'incomes-page' : IncomesPage,
}

Object.entries(mounts).forEach(([id, component]) => {
    const element = document.getElementById(id);
    
    if (!element) return;
    
    const props = element.dataset.props
        ? JSON.parse(element.dataset.props)
        : {};
    
    createApp(component, props).mount(element);
})

