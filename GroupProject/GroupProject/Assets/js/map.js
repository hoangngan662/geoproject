
listOfLocations=[
    {
        name:'Thiền viện Trúc Lâm Phương Nam',
        tag:"#ThienVien",
        keywords:'thien vien vien truc lam phuong nam',
        cords: [9.990357, 105.703923],
    },
    {
        name:'Khu Du Lịch Sinh Thái Ông Đề',
        tag:"#OngDe",
        keywords:'ong de',
        cords: [9.991795, 105.707766],
    },
    {
        name:'Du lịch sinh thái vườn trái cây Phi Yến',
        tag:"#PhiYen",
        keywords:'phi yen',
        cords: [9.971439, 105.694891],
    },
    {
        name:'Cồn Sơn',
        tag:"#ConSon",
        keywords:'con son',
        cords: [10.085012, 105.746047],
    },
    {
        name:'Bảo Gia Trang Viên',
        tag:"#BaoGiaTrang",
        keywords:'bao gia trang vien',
        cords: [9.960776, 105.758496],
    },
    {
        name:'Cồn Ấu',
        tag:"#ConAu",
        keywords:'con au',
        cords: [10.031590, 105.803533],
    },
    {
        name:'Vườn Sinh Thái Ba Láng',
        tag:"#BaLang",
        keywords:'ba lang',
        cords: [9.977994, 105.739620],
    },
    {
        name:'Khu du lịch Cần Thơ Hoa Sứ',
        tag:'#HoaSu',
        keywords:'hoa su',
        cords: [10.134672, 105.590375],
    },
    {
        name:'Vườn cò Bằng Lăng',
        tag:'#CoBangLang',
        keywords:'vuon co bang lang',
        cords: [10.281463, 105.505518],
    },
    {
        name: 'Vườn sinh thái Xẻo Nhum',
        tag: '#XeoNhum',
        keywords: 'xeo nhum',
        cords: [9.997829, 105.776990],
    }
]
const mymap = L.map('mapid').setView([10.029520, 105.770934], 11);
const attribution = 
    `&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors`
const tileUrl = `https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png`
const tiles = L.tileLayer(tileUrl,{attribution})
tiles.addTo(mymap)

//mymap.addControl(new L.Control.SearchMarker({toggleFullscreen: true}));
 mymap.addControl(new L.Control.Fullscreen({fullscreenControl: {
    pseudoFullscreen: true 
}}));



const markers=[]

listOfLocations.forEach((location)=>{
    const length = markers.push(L.marker(location.cords, {name: location.keywords}).addTo(mymap));
    const index = length -1;
    console.log(markers[index]);
    L.DomUtil.addClass(markers[index]._icon, 'fullscreenMarker');
    const item = document.querySelector(location.tag);
    if(item){
        var clone = item.cloneNode(true)
        markers[index].bindPopup(clone,{ maxWidth: 300, minWidth: 200, className:"fullscreenContent"});
    }
})


listOfLocations.forEach((location)=>{
    let length = markers.push(L.marker(location.cords, {name: location.keywords}).addTo(mymap));
    const index = length-1;
    L.DomUtil.addClass(markers[index]._icon, 'normalMarker');
    markers[index].bindPopup(`${location.name}`,{closeButton: false});
    markers[index].on('mouseover', function (e) {
        this.openPopup();
    });
    markers[index].on('mouseout', function (e) {
        this.closePopup();
    });
    markers[index].on('click', function(ev) {
        const yOffset = -100; 
        const element = document.querySelector(location.tag);
        const y = element.getBoundingClientRect().top + window.pageYOffset + yOffset;
        window.scrollTo({top: y});
    });
})


layerGroup = L.layerGroup(markers);
const searchController = new L.Control.SearchMarker({layer:layerGroup});
mymap.addControl(searchController);
searchController.startSearch();

const fullscreenMarkers = document.querySelectorAll('.fullscreenMarker');
const minimizedMarkers = document.querySelectorAll('.normalMarker')

// // `fullscreenchange` Event that's fired when entering or exiting fullscreen.
mymap.on('fullscreenchange', function () {
    if (mymap.isFullscreen()) {
        fullscreenMarkers.forEach(el=>el.style.display = 'block');
        minimizedMarkers.forEach(el=>el.style.display = 'none');
        mymap.setView([30,mymap.getCenter().lng],2)
        console.log('fullscreen')
    } else {
        fullscreenMarkers.forEach(el=>el.style.display = 'none');
        minimizedMarkers.forEach(el=>el.style.display = 'block');
        mymap.closePopup();
        mymap.setView([30,mymap.getCenter().lng],0)
        console.log('minimized')
    }
});
mymap.on('popupopen', ()=>{
    M.AutoInit();
})

mymap.on('drag', ()=>{
    let {lng} = mymap.getCenter();
    let x = Math.trunc(lng);
    markers.forEach((marker)=>{
        const {lat:markerLat, lng:markerLng}= marker.getLatLng();
        if(x - markerLng > 180){
            var newLatLng = new L.LatLng( markerLat, markerLng+360);
            marker.setLatLng(newLatLng); 
        }else if(lng - markerLng < -180){
            var newLatLng = new L.LatLng(markerLat, markerLng-360);
            marker.setLatLng(newLatLng); 
        }
    })
})