import{t as e}from"./dom-utils-2919d18e.js";import{d as t,o,r as i,e as s,a as r,c as a,g as n,b as l,i as d,C as u,f as c,L as g}from"./baseChart-e7078c08.js";import"./dom-95153cd1.js";import"./browser-a3d50e79.js";import"./_tslib-158249d5.js";import"./disposable-d2c2d283.js";import"./tooltip-7f828cf8.js";class p extends u{constructor(e,t,o){super(e,t,o)}get widget(){return super.widget}createWidgetBuilder(e,t,o){const i=t.series.map((e=>e.category))[0],s="pie"===i||"donut"===i?this._dxBlazorViz.viz.dxPieChart:this._dxBlazorViz.viz.dxChart;return function(t){t.type=i,o(new s(e,t))}}completeChartOptions(t,o,i){let s=!1;const r=window.getComputedStyle(document.body).getPropertyValue("color"),a=this._selectionController;a.setSeriesSelectionMode(o.seriesSelectionMode);const n=super.completeChartOptions(t,o,i);n.rotated=o.rotated,n.barGroupPadding=o.barGroupPadding,n.barGroupWidth=o.barGroupWidth,n.synchronizeMultiAxes=o.synchronizeMultiAxes,n.commonAxisSettings={label:{overlappingBehavior:"rotate",rotationAngle:45,font:{color:r,opacity:.75}}};const l=this.dotNetHelper;return n.onSeriesClick=e=>{var t;const o=e.target,i=null===(t=this._pointClickArgs)||void 0===t?void 0:t.target;l.invokeMethodAsync("OnSeriesClickAsync",o.index,null==i?void 0:i.data,null==i?void 0:i.tag),this._pointClickArgs=null,this._selectionController.selectSeriesAndPoint(o,i)},n.commonSeriesSettings={argumentField:"argument",valueField:"value",openValueField:"openValue",highValueField:"highValue",lowValueField:"lowValue",closeValueField:"closeValue",rangeValue1Field:"startValue",rangeValue2Field:"endValue",sizeField:"size",type:"bar"},n.onDrawn=async function(o){s||(s=!0,c(t,o.component,g.series,a),e(t,"dx-loading",!1))},n.valueAxis=o.valueAxis,n.panes=o.panes,n.argumentAxis=o.argumentAxis,n.argumentAxis&&(n.argumentAxis.categories=n.argumentAxis.categories||[]),n.zoomAndPan=o.zoomAndPan,n.scrollBar=o.scrollBar,n.argumentAxis?n.argumentAxis.aggregateByCategory=!0:n.argumentAxis={aggregateByCategory:!0},n.resizePanesOnZoom=!0,n.seriesSelectionMode=o.seriesSelectionMode,n}}const m={init:function(e,t,o){return e?d(e,t,o,((e,t,o)=>new p(e,t,o))):Promise.resolve()},dispose:t,onSeriesVisibleChanged:o,render:i,exportToFile:s,exportToBase64AndGetLengthAsync:r,clearBase64ImageData:a,getBase64ImageChunk:n,resetSelection:l};export{m as default};