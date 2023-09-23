<template>
    <svg v-if="isOpenEye" :width="(width || 25)" :height="(height || 25)" viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
        <g clip-path="url(#clip0_513_2)">
            <path d="M1.97531 12.7901C1.97531 12.7901 5.97531 4.79013 12.9753 4.79013C19.9753 4.79013 23.9753 12.7901 23.9753 12.7901C23.9753 12.7901 19.9753 20.7901 12.9753 20.7901C5.97531 20.7901 1.97531 12.7901 1.97531 12.7901Z" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M12.9753 15.7901C14.6322 15.7901 15.9753 14.447 15.9753 12.7901C15.9753 11.1333 14.6322 9.79013 12.9753 9.79013C11.3185 9.79013 9.97531 11.1333 9.97531 12.7901C9.97531 14.447 11.3185 15.7901 12.9753 15.7901Z" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        </g>
        <defs>
            <clipPath id="clip0_513_2">
                <rect width="24" height="24" fill="white" transform="translate(0.975311 0.790131)"/>
            </clipPath>
        </defs>
    </svg>

    <svg v-else-if="isCloseEye" :width="(width || 25)" :height="(height || 25)" viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
        <g clip-path="url(#clip0_304_2)">
            <path d="M14.4604 14.9391C14.1858 15.2339 13.8546 15.4703 13.4866 15.6343C13.1186 15.7982 12.7213 15.8864 12.3185 15.8935C11.9157 15.9006 11.5156 15.8265 11.142 15.6756C10.7685 15.5248 10.4291 15.3002 10.1443 15.0153C9.85939 14.7304 9.63482 14.3911 9.48393 14.0175C9.33305 13.644 9.25895 13.2439 9.26606 12.8411C9.27316 12.4382 9.36133 12.041 9.5253 11.673C9.68927 11.305 9.92568 10.9738 10.2204 10.6991M18.2804 18.7591C16.571 20.0621 14.4895 20.784 12.3404 20.8191C5.34042 20.8191 1.34042 12.8191 1.34042 12.8191C2.58431 10.501 4.30956 8.47575 6.40042 6.87915L18.2804 18.7591ZM10.2404 5.05915C10.9288 4.89803 11.6335 4.81749 12.3404 4.81915C19.3404 4.81915 23.3404 12.8191 23.3404 12.8191C22.7334 13.9548 22.0095 15.0239 21.1804 16.0091L10.2404 5.05915Z" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M1.34042 1.81915L23.3404 23.8192" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        </g>
        <defs>
            <clipPath id="clip0_304_2">
                <rect width="24" height="24" fill="white" transform="translate(0.340424 0.819153)"/>
            </clipPath>
        </defs>
    </svg>

    <svg v-else-if="isSearch" :width="(width || 25)" :height="(height || 25)" viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M11.2778 19.0142C15.696 19.0142 19.2778 15.4325 19.2778 11.0142C19.2778 6.59591 15.696 3.01419 11.2778 3.01419C6.85949 3.01419 3.27777 6.59591 3.27777 11.0142C3.27777 15.4325 6.85949 19.0142 11.2778 19.0142Z" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        <path d="M21.2778 21.0142L16.9278 16.6642" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
    </svg>

    <svg v-else-if="isMenu" :width="(width || 25)" :height="(height || 25)" viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M10.25 3.875H3.25V10.875H10.25V3.875Z" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        <path d="M21.25 3.875H14.25V10.875H21.25V3.875Z" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        <path d="M21.25 14.875H14.25V21.875H21.25V14.875Z" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        <path d="M10.25 14.875H3.25V21.875H10.25V14.875Z" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
    </svg>

    <svg v-else-if="isClose" :width="(width || 24)" :height="(height || 24)" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M18 6L6 18" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        <path d="M6 6L18 18" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
    </svg>

    <div v-else></div>
</template>

<script src="@/components/Svg/Svg.ts" lang="ts"></script>