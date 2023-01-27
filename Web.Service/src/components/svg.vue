<template>
    <svg v-if="name == 'open-eye'" :width="width" :height="height" viewBox="0 0 60 41" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M30.0873 0.213974C16.754 0.213974 5.36733 8.50731 0.753998 20.214C5.36733 31.9206 16.754 40.214 30.0873 40.214C43.4207 40.214 54.8073 31.9206 59.4207 20.214C54.8073 8.50731 43.4207 0.213974 30.0873 0.213974ZM30.0873 33.5473C22.7273 33.5473 16.754 27.574 16.754 20.214C16.754 12.854 22.7273 6.88064 30.0873 6.88064C37.4473 6.88064 43.4207 12.854 43.4207 20.214C43.4207 27.574 37.4473 33.5473 30.0873 33.5473ZM30.0873 12.214C25.6607 12.214 22.0873 15.7873 22.0873 20.214C22.0873 24.6406 25.6607 28.214 30.0873 28.214C34.514 28.214 38.0873 24.6406 38.0873 20.214C38.0873 15.7873 34.514 12.214 30.0873 12.214Z" fill="black" />
    </svg>

    <svg v-else-if="name == 'close-eye'" :width="width" :height="height" viewBox="0 0 60 51" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M30.0873 10.8806C37.4473 10.8806 43.4207 16.854 43.4207 24.214C43.4207 25.9473 43.074 27.574 42.4607 29.094L50.2473 36.8806C54.274 33.5206 57.4473 29.174 59.394 24.214C54.7807 12.5073 43.394 4.21397 30.0607 4.21397C26.3273 4.21397 22.754 4.88064 19.4473 6.08064L25.2073 11.8406C26.7273 11.2273 28.354 10.8806 30.0873 10.8806ZM3.42066 3.60064L9.50067 9.68064L10.7273 10.9073C6.30066 14.3473 2.834 18.934 0.753998 24.214C5.36733 35.9206 16.754 44.214 30.0873 44.214C34.2207 44.214 38.1673 43.414 41.7673 41.974L42.8873 43.094L50.7007 50.8806L54.0873 47.494L6.80733 0.213974L3.42066 3.60064ZM18.1673 18.3473L22.3007 22.4806C22.1673 23.0406 22.0873 23.6273 22.0873 24.214C22.0873 28.6406 25.6607 32.214 30.0873 32.214C30.674 32.214 31.2607 32.134 31.8207 32.0006L35.954 36.134C34.1673 37.014 32.194 37.5473 30.0873 37.5473C22.7273 37.5473 16.754 31.574 16.754 24.214C16.754 22.1073 17.2873 20.134 18.1673 18.3473V18.3473ZM29.6607 16.2673L38.0607 24.6673L38.114 24.2406C38.114 19.814 34.5407 16.2406 30.114 16.2406L29.6607 16.2673Z" fill="black" />
    </svg>

    <svg v-else-if="name == 'arrow-right'" :width="width" :height="height" viewBox="0 0 21 33" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M20.0933 28.4321L7.87999 16.1921L20.0933 3.95214L16.3333 0.19214L0.333328 16.1921L16.3333 32.1921L20.0933 28.4321Z" fill="black" />
    </svg>

    <svg v-else-if="name == 'arrow-left'" :width="width" :height="height" viewBox="0 0 21 33" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M0.906662 28.7655L13.12 16.5255L0.906662 4.28547L4.66666 0.525473L20.6667 16.5255L4.66666 32.5255L0.906662 28.7655Z" fill="black" />
    </svg>

    <svg v-else-if="name == 'arrow-top'" :width="width" :height="height" viewBox="0 0 32 21" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M3.76 20.2855L16 8.07214L28.24 20.2855L32 16.5255L16 0.525473L0 16.5255L3.76 20.2855Z" fill="black" />
    </svg>

    <svg v-else-if="name == 'arrow-bottom'" :width="width" :height="height" viewBox="0 0 32 20" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M3.76 0.0988083L16 12.3121L28.24 0.0988083L32 3.85881L16 19.8588L0 3.85881L3.76 0.0988083Z" fill="black" />
    </svg>

    <svg v-else-if="name == 'close'" :width="width" :height="height" viewBox="0 0 64 64" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M50.6667 17.0933L46.9067 13.3333L32 28.24L17.0933 13.3333L13.3333 17.0933L28.24 32L13.3333 46.9067L17.0933 50.6667L32 35.76L46.9067 50.6667L50.6667 46.9067L35.76 32L50.6667 17.0933Z" fill="black" />
    </svg>

    <svg v-else-if="name == 'search'" :width="width" :height="height" viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M11.75 19.875C16.1683 19.875 19.75 16.2933 19.75 11.875C19.75 7.45672 16.1683 3.875 11.75 3.875C7.33172 3.875 3.75 7.45672 3.75 11.875C3.75 16.2933 7.33172 19.875 11.75 19.875Z" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        <path d="M21.75 21.875L17.4 17.525" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
    </svg>

    <svg v-else-if="name == 'upload'" :width="width" :height="height" viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M21.3053 15.0674V19.0674C21.3053 19.5978 21.0946 20.1066 20.7195 20.4816C20.3444 20.8567 19.8357 21.0674 19.3053 21.0674H5.30529C4.77486 21.0674 4.26615 20.8567 3.89108 20.4816C3.516 20.1066 3.30529 19.5978 3.30529 19.0674V15.0674" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        <path d="M17.3053 8.06741L12.3053 3.06741L7.30529 8.06741" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        <path d="M12.3053 3.06741V15.0674" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
    </svg>

    <svg v-else-if="name == 'download'" :width="width" :height="height" viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M21.3053 15.0674V19.0674C21.3053 19.5978 21.0946 20.1066 20.7195 20.4816C20.3444 20.8567 19.8357 21.0674 19.3053 21.0674H5.30529C4.77486 21.0674 4.26615 20.8567 3.89108 20.4816C3.516 20.1066 3.30529 19.5978 3.30529 19.0674V15.0674" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        <path d="M7.30529 10.0674L12.3053 15.0674L17.3053 10.0674" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        <path d="M12.3053 15.0674V3.06741" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
    </svg>

    <svg v-else-if="name == 'option'" :width="width" :height="height" viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
        <path d="M10.25 3.875H3.25V10.875H10.25V3.875Z" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        <path d="M21.25 3.875H14.25V10.875H21.25V3.875Z" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        <path d="M21.25 14.875H14.25V21.875H21.25V14.875Z" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        <path d="M10.25 14.875H3.25V21.875H10.25V14.875Z" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
    </svg>

    <svg v-else-if="name == 'persons'" :width="width" :height="height" viewBox="0 0 25 25" fill="none" xmlns="http://www.w3.org/2000/svg">
        <g clip-path="url(#clip0_412_5)">
            <path d="M17.8565 21.5095V19.5095C17.8565 18.4487 17.4351 17.4312 16.6849 16.6811C15.9348 15.931 14.9174 15.5095 13.8565 15.5095H5.85651C4.79564 15.5095 3.77822 15.931 3.02808 16.6811C2.27793 17.4312 1.85651 18.4487 1.85651 19.5095V21.5095" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M9.85651 11.5095C12.0656 11.5095 13.8565 9.71867 13.8565 7.50953C13.8565 5.30039 12.0656 3.50953 9.85651 3.50953C7.64737 3.50953 5.85651 5.30039 5.85651 7.50953C5.85651 9.71867 7.64737 11.5095 9.85651 11.5095Z" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M23.8565 21.5095V19.5095C23.8558 18.6233 23.5609 17.7623 23.0179 17.0618C22.4749 16.3614 21.7146 15.8611 20.8565 15.6395" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            <path d="M16.8565 3.63953C17.7169 3.85983 18.4795 4.36023 19.0241 5.06184C19.5687 5.76345 19.8643 6.62636 19.8643 7.51453C19.8643 8.4027 19.5687 9.26561 19.0241 9.96722C18.4795 10.6688 17.7169 11.1692 16.8565 11.3895" stroke="black" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        </g>

        <defs>
            <clipPath id="clip0_412_5">
               <rect width="24" height="24" fill="white" transform="translate(0.856506 0.509529)"/>
            </clipPath>
        </defs>
    </svg>

</template>

<script>
const SvgImage = {
    props: {
        name: {
            type: String,
            required: false
        },

        width: {
            type: String,
            required: false
        },

        height: {
            type: String,
            required: false
        },
    }
}

export default SvgImage
</script>
