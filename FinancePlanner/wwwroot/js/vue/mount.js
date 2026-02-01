console.log('mount.js loaded');
const { createApp } = Vue;
import DashboardPage from './pages/DashboardPage.js';

const mounts = {
    'dashboard-page' : DashboardPage,
}

Object.entries(mounts).forEach(([id, component]) => {
    const element = document.getElementById(id);
    
    if (!element) return;
    
    const props = element.dataset.props
        ? JSON.parse(element.dataset.props)
        : {};
    
    createApp(component, props).mount(element);
})

