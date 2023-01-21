// тут будет фигня для подменю
    function submenu(s) {
        const a = document.getElementById(s);
        if (a.style.visibility == 'hidden') {
            a.style.visibility = 'visible';
            a.style.opacity = 1;
        }
        else {
            a.style.visibility = 'hidden';
            a.style.opacity = 0;
        }
    
        return false;
    }
